import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'grid-header',
  standalone: true,
  imports: [],
  templateUrl: './grid-header.component.html',
  styleUrl: './grid-header.component.css'
})
export class GridHeaderComponent {
  @Output() addClicked = new EventEmitter<void>();
  @Output() editClicked = new EventEmitter<void>();
  @Output() deleteClicked = new EventEmitter<void>();
  @Output() copyClicked = new EventEmitter<void>();
  @Output() importClicked = new EventEmitter<void>();
  @Output() exportTemplateClicked = new EventEmitter<void>();
  @Output() exportWithGridDataClicked = new EventEmitter<void>();
  @Output() exportWithOriginalDataClicked = new EventEmitter<void>();

  add() {
      this.addClicked.emit();
  }

  edit() {
      this.editClicked.emit();
  }

  delete() {
      this.deleteClicked.emit();
  }

  copy() {
      this.copyClicked.emit();
  }

  import() {
      this.importClicked.emit();
  }

  exportTemplate() {
      this.exportTemplateClicked.emit();
  }

  exportWithGridData() {
      this.exportWithGridDataClicked.emit();
  }

  exportWithOriginalData() {
      this.exportWithOriginalDataClicked.emit();
  }
}
