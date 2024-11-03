import { Component, Input, OnInit } from '@angular/core';
import { PolicyType } from '../models/policytype';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'add-edit-policy-type',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './addeditpolicytype.component.html',
  styleUrls: ['./addeditpolicytype.component.css']
})
export class AddEditPolicyTypeComponent implements OnInit {
  @Input() policyType: PolicyType = {} as PolicyType; 
  isSidebarVisible = false;

  ngOnInit() {
    if (!this.policyType) {
      this.policyType = { policyTypeId: 0, name: '', code: '', createdOn: '', modifiedOn: '', isActive: true };
    }
  }

  showSidebar() {
    this.isSidebarVisible = true;
    const backdrop = document.createElement('div');
    backdrop.className = 'modal-backdrop fade show';
    document.body.appendChild(backdrop);
  }

  closeSidebar() {
    this.isSidebarVisible = false;
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
    this.resetForm();
  }
  resetForm() {
    this.policyType = { policyTypeId: 0, name: '', code: '', createdOn: '', modifiedOn: '', isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.policyType);
    this.closeSidebar();
  }
}