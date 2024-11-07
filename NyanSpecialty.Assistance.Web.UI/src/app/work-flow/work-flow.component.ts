import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddEditWorkflowComponent } from './add-edit-workflow.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';

@Component({
  selector: 'app-work-flow',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    AddEditWorkflowComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent],
  templateUrl: './work-flow.component.html',
  styleUrl: './work-flow.component.css'
})
export class WorkFlowComponent implements OnInit {
  ngOnInit(): void {

  }
}
