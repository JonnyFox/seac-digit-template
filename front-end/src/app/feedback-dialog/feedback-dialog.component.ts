import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';



  @Component({
    selector: 'app-feedback-dialog.component',
    templateUrl: 'feedback-dialog.component.html',
    styleUrls: ['feedback-dialog.component.scss']
  })
  export class FeedbackDialogComponent {

    constructor(
      public dialogRef: MatDialogRef<FeedbackDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: any) { }

    onNoClick(): void {
      this.dialogRef.close();
    }
}
