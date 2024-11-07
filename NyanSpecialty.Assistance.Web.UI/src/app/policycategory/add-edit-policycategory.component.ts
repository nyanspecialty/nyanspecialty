import { Component, EventEmitter,Input, OnInit, Output } from '@angular/core';
import { PolicyCategory } from '../models/policycategory';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-policycategory',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-edit-policycategory.component.html',
  styleUrl: './add-edit-policycategory.component.css'
})
export class AddEditPolicyCategoryComponent implements OnInit {

  isSidebarVisible: boolean = false;

  @Input() policyCategory: PolicyCategory = {} as PolicyCategory;

  @Output() policyCategoryChange = new EventEmitter<PolicyCategory>();
 
  @Output() cancel = new EventEmitter<void>();

  ngOnInit() {
    if (!this.policyCategory) {
      this.policyCategory = { policyCategoryId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
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
  hideSideBar(){
    this.isSidebarVisible = false;
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
    this.resetForm();
  }
  resetForm() {
    this.policyCategory = { policyCategoryId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.policyCategory);
    this.policyCategoryChange.emit(this.policyCategory);
    this.closeSidebar();
  }
}
