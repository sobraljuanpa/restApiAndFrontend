import { Component, OnInit } from '@angular/core';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Location } from '@angular/common';
import { MigrationService } from '../../services/migration.service';

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
    private location: Location
  ) {}

  ngOnInit() {
  }

  importFile(){
    console.log("["+this.path+"]");
    this.migrationService.addMigration(this.selectedType, "["+this.path+"]").subscribe(
      res => {
        this.location.back();
      }
    )
  }

}
