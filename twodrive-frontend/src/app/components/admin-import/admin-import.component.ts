import { Component, OnInit } from '@angular/core';
import { analyzeAndValidateNgModules } from '@angular/compiler';
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
  typeImport: string[] = ['Xml','Json'];

  constructor(
    private migrationService: MigrationService,
    private location: Location,
    private alertService: AlertService
  ) {}

  ngOnInit() {
  }

  importFile(){
    console.log("["+this.path+"]");
    this.migrationService.addMigration(this.selectedType, "["+this.path+"]").subscribe(
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
