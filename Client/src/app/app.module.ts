import { NgModule } from '@angular/core';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from 'src/app/app.component';
import { SharedModule } from 'src/app/shared/shared.module';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthenticationInterceptor } from 'src/app/shared/interceptors/authentication-interceptor';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
   declarations: [
      AppComponent,
   ],
   imports: [
      AppRoutingModule,
      BrowserAnimationsModule,
      HttpClientModule,
      SharedModule
   ],
   providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true
      },
      CookieService
   ],
   bootstrap: [
      AppComponent
   ]
})

export class AppModule { }
