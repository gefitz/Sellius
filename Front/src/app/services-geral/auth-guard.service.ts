import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  GuardResult,
  MaybeAsync,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate {
  constructor(private cookie: CookieService, private router: Router) {}
  canActivate(): boolean {
    if (this.cookie.check('auth_token')) {
      return true;
    }
    this.router.navigate(['/Login']);
    return false;
  }
}
