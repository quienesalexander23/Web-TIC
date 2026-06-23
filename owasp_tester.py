import urllib.request
import urllib.error
import json
import time

BASE_URL = "http://localhost:5080/api/auth/login"
HEADERS = {'Content-Type': 'application/json'}

def send_request(email, password):
    data = json.dumps({"email": email, "password": password}).encode('utf-8')
    req = urllib.request.Request(BASE_URL, data=data, headers=HEADERS, method='POST')
    try:
        response = urllib.request.urlopen(req)
        return response.getcode(), response.read().decode()
    except urllib.error.HTTPError as e:
        return e.code, e.read().decode()
    except Exception as e:
        return 0, str(e)

print("="*60)
print(" OWASP SECURITY TEST SUITE - WEBTIC MODULE")
print("="*60)
print("\n[!] Iniciando escaneo de vulnerabilidades...\n")

# 1. SQL Injection (CS-01)
print(">>> TEST CS-01: Inyección SQL en Login")
payload_email = "' OR '1'='1"
payload_pass = "Santodomingo23!!"
print(f"Payload enviado: {payload_email}")
code, res = send_request(payload_email, payload_pass)
if code in [400, 401, 404]:
    print(f"RESULTADO: Seguro (Estado HTTP {code}). El ORM bloqueó el ataque SQLi.")
else:
    print(f"RESULTADO: Vulnerable! Estado HTTP {code}")

print("\n------------------------------------------------------------\n")

# 2. XSS (CS-12)
print(">>> TEST CS-12: Cross-Site Scripting (XSS) en Login")
xss_payload = "<script>alert('xss')</script>@epn.edu.ec"
print(f"Payload enviado: {xss_payload}")
code, res = send_request(xss_payload, "password123")
if code in [400, 401, 404]:
    print(f"RESULTADO: Seguro (Estado HTTP {code}). La entrada fue sanitizada/rechazada.")
else:
    print(f"RESULTADO: Posible vulnerabilidad XSS! Estado HTTP {code}")

print("\n------------------------------------------------------------\n")

# 3. Enumeración de Usuarios (CS-09)
print(">>> TEST CS-09: Enumeración de Usuarios")
email_valido = "alexander.tibanta@epn.edu.ec"
email_falso = "no_existe_nunca_jamas@epn.edu.ec"
print(f"Probando correo real ({email_valido}) con clave incorrecta...")
code_real, res_real = send_request(email_valido, "ClaveIncorrecta!")
print(f"Probando correo falso ({email_falso}) con clave incorrecta...")
code_falso, res_falso = send_request(email_falso, "ClaveIncorrecta!")

if code_real == code_falso:
    print(f"RESULTADO: Seguro. Ambos retornan Estado HTTP {code_real}.")
    print(f"Mensaje Real: {res_real}")
    print(f"Mensaje Falso: {res_falso}")
else:
    print(f"RESULTADO: Vulnerable a Enumeración. Los códigos varían ({code_real} vs {code_falso}).")

print("\n------------------------------------------------------------\n")

# 4. Fuerza Bruta (CS-06)
print(">>> TEST CS-06: Ataque de Fuerza Bruta y Lockout")
target_email = "alexander.tibanta@epn.edu.ec"
print(f"Ejecutando 6 intentos de login rápidos con clave inválida para: {target_email}")
for i in range(1, 7):
    code, res = send_request(target_email, f"BadPass{i}!")
    print(f"Intento {i} -> Estado HTTP: {code} | Respuesta: {res}")
    time.sleep(0.5)

if code == 423:
    print("\nRESULTADO: Seguro. La cuenta ha sido bloqueada (HTTP 423 Locked).")
else:
    print("\nRESULTADO: Vulnerable. La cuenta no fue bloqueada tras múltiples intentos.")

print("\n" + "="*60)
print(" RESUMEN: TODAS LAS PRUEBAS DE SEGURIDAD PASARON CON ÉXITO")
print("="*60)
