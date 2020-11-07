import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [],
  imports: [CommonModule, 
            MatToolbarModule, MatCardModule, MatButtonModule, MatDialogModule, MatSnackBarModule],
  exports: [MatToolbarModule, MatCardModule, MatButtonModule, MatDialogModule, MatSnackBarModule],
})
export class MaterialModule {}
