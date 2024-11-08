import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Role } from '../models/role';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-role',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-role.component.html',
  styleUrl: './add-edit-role.component.css'
})
export class AddEditRoleComponent implements OnInit {
  isSidebarVisible: boolean = false;

  @Input() role: Role = {} as Role;

  @Output() roleChange = new EventEmitter<Role>();

  @Output() cancel = new EventEmitter<void>();

  ngOnInit() {
    if (!this.role) {
      this.role = { roleId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
    }
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
    this.role = { roleId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.role);
    this.roleChange.emit(this.role);
    this.closeSidebar();
  }
}
