import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { AddEditRoleComponent } from './add-edit-role.component';
import { Role } from '../models/role';
import { ToastrService } from 'ngx-toastr';
import { RoleService } from '../services/role.service';
import { CommonPropsService } from '../services/common.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    AddEditRoleComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    NgxDatatableModule],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class RoleComponent implements OnInit {
  roles: Role[] = [];

  selectedItems: Role[] = [];

  filteredRows: Role[] = [];

  @ViewChild(AddEditRoleComponent) addEditRoleComponent!: AddEditRoleComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedRole: Role = {} as Role;

  newRole: Role = {} as Role;

  constructor(private loaderSerivce: LoaderService,
    private tosterService: ToastrService,
    private roleService: RoleService,
    private commonPropsService: CommonPropsService
  ) {

  }
  ngOnInit() {
    this.loaderSerivce.showLoader();
    this.fecthRolesAsync();
  }
  fecthRolesAsync() {
    this.roleService.fetchRolesAsync().subscribe(response => {
      this.roles = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as Role[];
      this.filteredRows = this.roles;
      this.loaderSerivce.hideLoader();
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditRoleComponent.role = this.newRole;
    this.addEditRoleComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditRoleComponent.role = this.selectedRole;
    this.addEditRoleComponent.showSidebar();
    this.selectedRole = {} as Role;
  }
  onCloseSidebar() {
    this.addEditRoleComponent.role = this.newRole;
    this.addEditRoleComponent.hideSideBar();
    this.selectedRole = {} as Role;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderSerivce.showLoader();
    this.selectedRole.isActive = false;
    this.insertOrUpdateRoleAsync(this.commonPropsService.prepareModelForSave(this.selectedRole), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderSerivce.showLoader();
    this.selectedRole.roleId = 0;
    this.insertOrUpdateRoleAsync(this.commonPropsService.prepareModelForSave(this.selectedRole), false, false, true, false);
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
    this.roles.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.roles;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.roles.every(row => row.isChecked);
    const anyUnchecked = this.roles.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedRole = row;
      this.isItemSelected = true;
      const multipleWorkflows: Role[] = [];
      multipleWorkflows.push(row);
      this.addOrUpdateWorkflows(multipleWorkflows);
    } else {
      this.isItemSelected = false;
      this.selectedRole = {} as Role;
      this.selectedItems = this.selectedItems.filter(item => item.roleId !== row.roleId);
    }
  }

  isAllChecked() {
    return this.roles.every(row => row.isChecked);
  }
  addOrUpdateWorkflows(items: Role[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.roleId === item.roleId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleRoleChange(role: Role) {
    console.log(role);
    this.loaderSerivce.showLoader();
    const isAdd = role.roleId === 0;
    const isUpdate = role.roleId > 0;
    role.isActive = true;
    this.insertOrUpdateRoleAsync(this.commonPropsService.prepareModelForSave(role), isAdd, isUpdate, false, false);

  }
  insertOrUpdateRoleAsync(role: Role, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.roleService.insertOrUpdateRoleAsync(role).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("role added sucessful");
        else if (isUpdate)
          this.tosterService.success("role updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedRole = {} as Role;
          this.showCopyConfirmModal = false;
          this.tosterService.success("role copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedRole = {} as Role;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("role deleted sucessful");
        }
      }
      this.fecthRolesAsync();
      this.loaderSerivce.hideLoader();
    });
  }
}