import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectTableEntry } from 'src/app/core/models/project-table-entry.model';
import { DateParserService } from 'src/app/core/services/date-parser.service';

const Priority = [
  { value: 0, name: 'Default' },
  { value: 1, name: 'Important' },
  { value: 2, name: 'Leading'}
];

@Component({
  selector: 'app-project-table-entry-detail-modal',
  templateUrl: './project-table-entry-detail-modal.component.html',
  styleUrls: ['./project-table-entry-detail-modal.component.css']
})
export class ProjectTableEntryDetailModalComponent implements OnInit {
  public entry: ProjectTableEntry;
  public entryForm: FormGroup;
  public priority = Priority;

  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal, private dateService: DateParserService) { }

  ngOnInit() {
    this.showNotification = false;

    this.entryForm = new FormGroup({
      name: new FormControl(this.entry.name),
      content: new FormControl(this.entry.content),
      priority: new FormControl(this.entry.priority)
    });
  }

  public userFinished(): string {
    if(this.entry.userFinished != null) {
      if(this.entry.userFinished.team != null) {
        return `${ this.entry.userFinished.name } - ${ this.entry.userFinished.team.name }`;
      }
      return `${ this.entry.userFinished.name }`;
    }
    return 'Unknown';
  }

  public deadlineInDays(): number {
    return Math.abs(this.dateService.inDays(new Date(this.entry.endingDate)));
  }

  public isDeadline(): boolean {
    return this.dateService.inDays(new Date(this.entry.endingDate)) <= 0;
  }

  public entryPatchSubmit() {
    if(this.entryForm.valid) {
      this.entry.name = this.entryForm.controls['name'].value;
      this.entry.content = this.entryForm.controls['content'].value;
      this.entry.priority = this.entryForm.controls['priority'].value;
      
      this.modal.close(this.entry);
    }
  }

  public userTeamColor(): string {
    if(this.entry.userFinished.team != null) {
      const teamColor = this.entry.userFinished.team.color;
      if(teamColor.length) {
        return `${teamColor}`;
      }
    }
    return '#000';
  }
}
