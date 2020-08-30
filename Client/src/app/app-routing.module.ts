import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: 'account', loadChildren: () => import('src/app/account/account.module').then( x => x.AccountModule)},
  {path: 'user', loadChildren: () => import('src/app/user/user.module').then(x => x.UserModule)},
  { path: '',   redirectTo: '/account/logIn', pathMatch: 'full' }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    onSameUrlNavigation: 'reload'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
