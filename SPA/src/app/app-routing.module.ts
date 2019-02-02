import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DealerComponent } from './dealer/dealer.component';

const routes: Routes = [
  { path: '', redirectTo: '/dealer', pathMatch: 'full' },
  { path: 'dealer', component: DealerComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
