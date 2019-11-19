import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MigrationService } from '../../services/migration.service';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-admin-import',
  templateUrl: './admin-import.component.html',
  styleUrls: ['./admin-import.component.css']
})
export class AdminImportComponent implements OnInit {
  selectedType: string;
  path: string;
  array: string[];
  typeImport: string[] = ['Xml','Json'];

  constructor(
    private migrationService: MigrationService,
    private location: Location,
    private alertService: AlertService
  ) {}

  ngOnInit() {
  }

  importFile(){
    this.array = [this.path];
    this.migrationService.addMigration(this.selectedType, this.array).subscribe(
      res => {
        this.alertService.success("Importing successfully!")
        this.location.back();
      },
      err => {
        this.alertService.warning("Importing failed!")
      }
    )
  }

}
