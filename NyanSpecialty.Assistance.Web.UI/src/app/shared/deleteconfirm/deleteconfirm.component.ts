import { Component, EventEmitter,  Output } from '@angular/core';

@Component({
  selector: 'app-deleteconfirm',
  standalone: true,
  imports: [],
  templateUrl: './deleteconfirm.component.html',
  styleUrl: './deleteconfirm.component.css'
})
export class DeleteconfirmComponent {
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit(); // Emit confirm event
  }

  onCancel() {
    this.cancel.emit(); // Emit cancel event
  }
}
