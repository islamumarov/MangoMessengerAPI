import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokensService } from '../services/messenger/tokens.service';

@Injectable()
export class RequestHeaderInterceptor implements HttpInterceptor {
  constructor(
    private tokensService: TokensService,
    private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const accessToken = this.tokensService.getTokens()?.accessToken;

    if (accessToken === undefined) {
      this.router.navigateByUrl("login");
    }

    const addHeaderRequest = request.clone({
      headers: new HttpHeaders({ Authorization: 'Bearer ' + accessToken })
    });

    return next.handle(addHeaderRequest);
  }
}
