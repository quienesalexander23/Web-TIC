// QA-DEBUG-TEMPORAL: reverse proxy so the browser only talks to one origin.
// Serves the built Angular app and forwards /api/* to the real backend on :5080.
const http = require('http');
const httpProxy = (() => {
  return function proxy(req, res, target) {
    const url = new URL(req.url, target);
    const options = {
      hostname: url.hostname,
      port: url.port,
      path: req.url,
      method: req.method,
      headers: { ...req.headers, host: url.host },
    };
    const proxyReq = http.request(options, (proxyRes) => {
      res.writeHead(proxyRes.statusCode, proxyRes.headers);
      proxyRes.pipe(res, { end: true });
    });
    proxyReq.on('error', (err) => {
      res.writeHead(502, { 'Content-Type': 'text/plain' });
      res.end('Proxy error: ' + err.message);
    });
    req.pipe(proxyReq, { end: true });
  };
})();

const fs = require('fs');
const path = require('path');
const STATIC_ROOT = path.join(__dirname, 'dist', 'webtic-frontend-app', 'browser');
const BACKEND = 'http://localhost:5080';

const MIME = {
  '.html': 'text/html', '.js': 'application/javascript', '.css': 'text/css',
  '.png': 'image/png', '.ico': 'image/x-icon', '.svg': 'image/svg+xml',
  '.json': 'application/json', '.woff2': 'font/woff2',
};

const server = http.createServer((req, res) => {
  if (req.url.startsWith('/api/')) {
    return httpProxy(req, res, BACKEND);
  }
  let filePath = path.join(STATIC_ROOT, req.url.split('?')[0]);
  if (req.url === '/' || !fs.existsSync(filePath) || fs.statSync(filePath).isDirectory()) {
    filePath = path.join(STATIC_ROOT, 'index.html');
  }
  fs.readFile(filePath, (err, data) => {
    if (err) {
      res.writeHead(404);
      return res.end('Not found');
    }
    const ext = path.extname(filePath);
    res.writeHead(200, { 'Content-Type': MIME[ext] || 'application/octet-stream' });
    res.end(data);
  });
});

const PORT = 4400;
server.listen(PORT, () => console.log(`QA proxy listening on http://localhost:${PORT}`));
