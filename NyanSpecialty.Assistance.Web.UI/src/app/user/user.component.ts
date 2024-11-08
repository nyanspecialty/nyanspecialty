import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { AddEditUserComponent } from './add-edit-user.component';
import { UserService } from '../services/user.service';
import { CommonPropsService } from '../services/common.service';
import { UserInfirmation } from '../models/userinfirmation';
import { UserRegistration } from '../models/userregistration';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    AddEditUserComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    NgxDatatableModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UserComponent implements OnInit {

  userInfirmation: UserInfirmation[] = [];

  selectedItems: UserInfirmation[] = [];

  filteredusers: UserInfirmation[] = [];

  @ViewChild(AddEditUserComponent) addEditUserComponent!: AddEditUserComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedUser: UserInfirmation = {} as UserInfirmation;

  selectedUserRegistraion: UserRegistration = {} as UserRegistration;

  newUserRegistration: UserRegistration = {} as UserRegistration;

  constructor(private loaderSerivce: LoaderService,
    private tosterService: ToastrService,
    private userService: UserService,
    private commonPropsService: CommonPropsService
  ) {

  }
  ngOnInit() {
    this.loaderSerivce.showLoader();
    this.fecthServiceProvidersAsync();
  }
  fecthServiceProvidersAsync() {
    this.userService.fetchUsersAsync().subscribe(response => {
      this.userInfirmation = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as UserInfirmation[];
      this.filteredusers = this.userInfirmation;
      this.loaderSerivce.hideLoader();
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditUserComponent.userRegistration = this.newUserRegistration;
    this.addEditUserComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditUserComponent.userRegistration = this.selectedUserRegistraion;
    this.addEditUserComponent.showSidebar();
    this.selectedUser = {} as UserInfirmation;
  }
  onCloseSidebar() {
    this.addEditUserComponent.userRegistration = this.newUserRegistration;
    this.addEditUserComponent.hideSideBar();
    this.selectedUser = {} as UserInfirmation;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderSerivce.showLoader();
    this.selectedUser.isActive = false;
    this.insertOrUpdateUserRegistrationAsync(this.commonPropsService.prepareModelForSave(this.selectedUserRegistraion), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderSerivce.showLoader();
    this.selectedUser.id = 0;
    this.insertOrUpdateUserRegistrationAsync(this.commonPropsService.prepareModelForSave(this.selectedUserRegistraion), false, false, true, false);
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
    this.userInfirmation.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.userInfirmation;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.userInfirmation.every(row => row.isChecked);
    const anyUnchecked = this.userInfirmation.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedUser = row;
      this.isItemSelected = true;
      const multipleSUserInfirmations: UserInfirmation[] = [];
      multipleSUserInfirmations.push(row);
      this.addOrUpdateServiceProviders(multipleSUserInfirmations);
    } else {
      this.isItemSelected = false;
      this.selectedUser = {} as UserInfirmation;
      this.selectedItems = this.selectedItems.filter(item => item.providerId !== row.providerId);
    }
  }

  isAllChecked() {
    return this.userInfirmation.every(row => row.isChecked);
  }
  addOrUpdateServiceProviders(items: UserInfirmation[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.id === item.id);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleUserRegistrationChange(userRegistration: UserRegistration) {
    console.log(userRegistration);
    this.loaderSerivce.showLoader();
    const isAdd = userRegistration.id === 0;
    const isUpdate = userRegistration.id > 0;
    userRegistration.isActive = true;
    this.insertOrUpdateUserRegistrationAsync(this.commonPropsService.prepareModelForSave(userRegistration), isAdd, isUpdate, false, false);

  }
  insertOrUpdateUserRegistrationAsync(userRegistration: UserRegistration, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.userService.insertOrUpdateUserAsync(userRegistration).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("role added sucessful");
        else if (isUpdate)
          this.tosterService.success("role updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedUser = {} as UserInfirmation;
          this.showCopyConfirmModal = false;
          this.tosterService.success("role copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedUser = {} as UserInfirmation;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("role deleted sucessful");
        }
      }
      this.fecthServiceProvidersAsync();
      this.loaderSerivce.hideLoader();
    });
  }
}
