import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  @Input() content: string;
  @Input() type: string;

  constructor() { }

  ngOnInit(): void {
  }

  public setClass(): string {
    switch(this.type) {
      case 'success': return 'alert alert-success';
      case 'danger': return 'alert alert-danger';
      case 'warning': return 'alert alert-warning';
      default: return 'alert alert-success';
    }
  }

}
