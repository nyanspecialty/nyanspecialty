import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Customers } from '../models/customers';
import { InsurancePolicy } from '../models/insurancepolicy';
import { InsurancePolicyService } from '../services/insurancepolicy.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-customer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-customer.component.html',
  styleUrl: './add-edit-customer.component.css'
})
export class AddEditCustomerComponent implements OnInit {



  isSidebarVisible: boolean = false;

  @Input() customer: Customers = {} as Customers;

  @Output() customerChange = new EventEmitter<Customers>();

  @Output() cancel = new EventEmitter<void>();

  insurancePolicies: InsurancePolicy[] = [];

  constructor(private insurancePolicyService: InsurancePolicyService) { }

  ngOnInit() {
    if (!this.customer) {
      this.customer = { customerID: 0, name: '', email: '', address: '', contactNumber: '', insurancePolicyID: 0, createdOn: new Date(), modifiedOn: new Date(), isActive: true };
    }
    this.insurancePolicyService.fetchInsurancePolicysAsync().subscribe(response => {
      this.insurancePolicies = response;
    });
  }

  showSidebar() {
    this.isSidebarVisible = true;
    const backdrop = document.createElement('div');
    backdrop.className = 'modal-backdrop fade show';
    document.body.appendChild(backdrop);
  }

  closeSidebar() {
    this.cancel.emit();
  }
  hideSideBar() {
    this.isSidebarVisible = false;
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
    this.resetForm();
  }
  resetForm() {
    this.customer = { customerID: 0, name: '', email: '', address: '', contactNumber: '', insurancePolicyID: 0, createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.customer);
    this.customerChange.emit(this.customer);
    this.closeSidebar();
  }
}
