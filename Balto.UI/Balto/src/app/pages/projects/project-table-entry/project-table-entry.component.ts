import { Component, Input, OnInit } from '@angular/core';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';

@Component({
  selector: 'app-project-table-entry',
  templateUrl: './project-table-entry.component.html',
  styleUrls: ['./project-table-entry.component.css']
})
export class ProjectTableEntryComponent implements OnInit {
  @Input() entry: ProjectTableEntry;

  constructor() { }

  ngOnInit(): void {
  }

}
