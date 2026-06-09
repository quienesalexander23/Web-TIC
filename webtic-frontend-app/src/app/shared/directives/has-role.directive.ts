import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appHasRole]',
  standalone: true
})
export class HasRoleDirective {
  private allowedRoles: string[] = [];

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef
  ) { }

  @Input() set appHasRole(roles: string[]) {
    this.allowedRoles = roles;
    this.updateView();
  }

  private updateView() {
    this.viewContainer.clear();
    if (this.checkRole()) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }

  private checkRole(): boolean {
    const token = sessionStorage.getItem('token');
    if (!token) return false;

    try {
      // Decode JWT Payload
      const payload = JSON.parse(atob(token.split('.')[1]));
      
      // En ASP.NET Core Identity, el rol suele venir en el claim 'role' o 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      let userRoles = payload['role'] || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || [];
      
      if (!Array.isArray(userRoles)) {
        userRoles = [userRoles];
      }

      return this.allowedRoles.some(role => userRoles.includes(role));
    } catch (e) {
      return false;
    }
  }
}
