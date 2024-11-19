import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Workflow } from '../models/workflow';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-workflow',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './add-edit-workflow.component.html',
  styleUrl: './add-edit-workflow.component.css'

})
export class AddEditWorkflowComponent implements OnInit {
  
  isSidebarVisible: boolean = false;

  @Input() workflow: Workflow = {} as Workflow;

  @Output() workFlowChange = new EventEmitter<Workflow>();
 
  @Output() cancel = new EventEmitter<void>();

  ngOnInit() {
    if (!this.workflow) {
      this.workflow = { workFlowId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
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
    this.workflow = { workFlowId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.workflow);
    this.workFlowChange.emit(this.workflow);
    this.closeSidebar();
  }
}
