import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserRegistration } from '../models/userregistration';
import { RoleService } from '../services/role.service';
import { CustomerService } from '../services/customers.service';
import { ServiceProviderService } from '../services/serviceprovider.service';
import { Role } from '../models/role';
import { Customers } from '../models/customers';
import { ServiceProvider } from '../models/serviceprovider';
import { forkJoin } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-user.component.html',
  styleUrl: './add-edit-user.component.css'
})
export class AddEditUserComponent implements OnInit {

  dbRoles: Role[] = [];

  dbCustomers: Customers[] = [];

  dbServiceProviders: ServiceProvider[] = [];

  isSidebarVisible: boolean = false;

  @Input() userRegistration: UserRegistration = {} as UserRegistration;

  @Output() userRegistrationChange = new EventEmitter<UserRegistration>();

  @Output() cancel = new EventEmitter<void>();

  constructor(private roleService: RoleService,
    private customerService: CustomerService,
    private servieProviderService: ServiceProviderService) {

  }

  ngOnInit() {
    if (!this.userRegistration) {
      this.userRegistration = {
        id: 0,
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
        password: '',
        roleId: 0,
        providerId: 0,
        customerId: 0,
        lastPasswordChangedOn: new Date(),
        isBlocked: false,
        createdOn: new Date(),
        modifiedOn: new Date(),
        isActive: true
      };
    }
    this.loadDropdownData();
  }
  loadDropdownData() {
    forkJoin({
      roles: this.roleService.fetchRolesAsync(),
      customers: this.customerService.fetchCustomersAsync(),
      serviceProviders: this.servieProviderService.fetcherviceProvidersAsync()
    }).subscribe({
      next: (results) => {
        this.dbRoles = results.roles;
        this.dbCustomers = results.customers;
        this.dbServiceProviders = results.serviceProviders;
      },
      error: (err) => {
        console.error('Error fetching dropdown data', err);
      }
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
    this.userRegistration = {
      id: 0,
      firstName: '',
      lastName: '',
      email: '',
      phone: '',
      password: '',
      roleId: 0,
      providerId: 0,
      customerId: 0,
      lastPasswordChangedOn: new Date(),
      isBlocked: false,
      createdOn: new Date(),
      modifiedOn: new Date(),
      isActive: true
    };
  }
  onSubmit() {
    console.log('Form submitted', this.userRegistration);
    this.userRegistrationChange.emit(this.userRegistration);
    this.closeSidebar();
  }
}
