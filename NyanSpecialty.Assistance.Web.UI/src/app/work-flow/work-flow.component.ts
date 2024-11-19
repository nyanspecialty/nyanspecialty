import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddEditWorkflowComponent } from './add-edit-workflow.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { WorkflowService } from '../services/workflow.service';
import { Workflow } from '../models/workflow';
import { CommonPropsService } from '../services/common.service';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-work-flow',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    AddEditWorkflowComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    NgxDatatableModule],
  templateUrl: './work-flow.component.html',
  styleUrl: './work-flow.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class WorkFlowComponent implements OnInit {

  workflows: Workflow[] = [];

  selectedItems: Workflow[] = [];

  filteredRows: Workflow[] = [];

  @ViewChild(AddEditWorkflowComponent) addEditWorkflowComponent!: AddEditWorkflowComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedWorkFlow: Workflow = {} as Workflow;

  newWorkFlow: Workflow = {} as Workflow;

  constructor(private loaderSerivce: LoaderService,
    private tosterService: ToastrService,
    private workFlowService: WorkflowService,
    private commonPropsService: CommonPropsService
  ) {

  }
  ngOnInit() {
    this.loaderSerivce.showLoader();
    this.fecthWorkflowsAsync();
  }
  fecthWorkflowsAsync() {
    this.workFlowService.fetchWorkflowsAsync().subscribe(response => {
      this.workflows = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as Workflow[];
      this.filteredRows = this.workflows;
      this.loaderSerivce.hideLoader();
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditWorkflowComponent.workflow = this.newWorkFlow;
    this.addEditWorkflowComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditWorkflowComponent.workflow = this.selectedWorkFlow;
    this.addEditWorkflowComponent.showSidebar();
    this.selectedWorkFlow = {} as Workflow;
  }
  onCloseSidebar() {
    this.addEditWorkflowComponent.workflow = this.newWorkFlow;
    this.addEditWorkflowComponent.hideSideBar();
    this.selectedWorkFlow = {} as Workflow;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderSerivce.showLoader();
    this.selectedWorkFlow.isActive = false;
    this.insertOrUpdateWorkFlowAsync(this.commonPropsService.prepareModelForSave(this.selectedWorkFlow), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderSerivce.showLoader();
    this.selectedWorkFlow.workFlowId = 0;
    this.insertOrUpdateWorkFlowAsync(this.commonPropsService.prepareModelForSave(this.selectedWorkFlow), false, false, true, false);
  }
  cancelCopy() {
    this.showCopyConfirmModal = false;
  }
  onImport() {

  }
  onExportTemplate() {

  }
  onExportWithGridData() {

  }
  onExportWithOriginalData() {

  }
  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.workflows.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.workflows;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.workflows.every(row => row.isChecked);
    const anyUnchecked = this.workflows.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedWorkFlow = row;
      this.isItemSelected = true;
      const multipleWorkflows: Workflow[] = [];
      multipleWorkflows.push(row);
      this.addOrUpdateWorkflows(multipleWorkflows);
    } else {
      this.isItemSelected = false;
      this.selectedWorkFlow = {} as Workflow;
      this.selectedItems = this.selectedItems.filter(item => item.workFlowId !== row.workFlowId);
    }
  }

  isAllChecked() {
    return this.workflows.every(row => row.isChecked);
  }
  addOrUpdateWorkflows(items: Workflow[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.workFlowId === item.workFlowId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleWorkflowChange(workflow: Workflow) {
    console.log(workflow);
    this.loaderSerivce.showLoader();
    const isAdd = workflow.workFlowId === 0;
    const isUpdate = workflow.workFlowId > 0;
    workflow.isActive = true;
    this.insertOrUpdateWorkFlowAsync(this.commonPropsService.prepareModelForSave(workflow), isAdd, isUpdate, false, false);

  }
  insertOrUpdateWorkFlowAsync(workFlow: Workflow, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.workFlowService.insertOrUpdateWorkflowAsync(workFlow).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("policy workflow added sucessful");
        else if (isUpdate)
          this.tosterService.success("policy workflow updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedWorkFlow = {} as Workflow;
          this.showCopyConfirmModal = false;
          this.tosterService.success("policy workflow copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedWorkFlow = {} as Workflow;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("policy workflow deleted sucessful");
        }
      }
      this.fecthWorkflowsAsync();
      this.loaderSerivce.hideLoader();
    });
  }
}
