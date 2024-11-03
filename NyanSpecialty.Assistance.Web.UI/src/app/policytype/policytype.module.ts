import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddEditPolicyTypeComponent } from './addeditpolicytype.component';
import { PolicytypeComponent } from './policytype.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';


@NgModule({
  declarations: [
    PolicytypeComponent,
    AddEditPolicyTypeComponent,
    GridHeaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule 
  ],
  exports: [
    PolicytypeComponent,
    AddEditPolicyTypeComponent,
    GridHeaderComponent 
  ]
})
export class PolicyTypeModule { }