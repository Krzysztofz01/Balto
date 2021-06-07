import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/core/models/project.model';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { ProjectService } from 'src/app/core/services/project.service';
import { TrelloIntegrationService } from 'src/app/core/services/trello-integration.service';
import { AddModalComponent } from './add-modal/add-modal.component';
import { AddTrelloModalComponent } from './add-trello-modal/add-trello-modal.component';
import { ProjectSyncService } from './project-sync/project-sync.service';
import { ViewSettings } from './view-settings.interface';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit, OnDestroy {
  public projects: Array<Project>;
  
  public selectedProject: Project;
  public selectProjectId: number;
  public syncSubscription: Subscription;

  //Settings related to project for individual client
  public entryCompactView: boolean;
  public tableTypeView: boolean;
  public viewSettings: ViewSettings;
  private readonly localStorageSettingsKey = "PROJECT_VIEW_SETTINGS";

  constructor(private projectService: ProjectService, private projectSyncService: ProjectSyncService, private trelloService: TrelloIntegrationService, private localStorage: LocalStorageService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadViewSettings();
    this.initializeProjects();

    this.syncSubscription = this.projectSyncService.project.subscribe(p => {
      this.selectedProject = p;
    });
  }

  ngOnDestroy(): void {
    this.projectSyncService.change(null);
    this.syncSubscription.unsubscribe();
  }

  private loadViewSettings(): void {
    const settings = this.localStorage.get(this.localStorageSettingsKey) as ViewSettings;
    if(settings != null) {
      this.viewSettings = settings;
      this.entryCompactView = settings.entryCompactView;
      this.tableTypeView = settings.tableTypeView;
    }
  }

  private initializeProjects(afterAdd: boolean = false, subjectId: number = null): void {
    this.projects = new Array<Project>();
    this.projectService.getAllProjects(1).subscribe((res) => {
      this.projects = res;

      //Set the first project by default
      if(!afterAdd) {
        const project = this.projects[0];
        if(project != null) {
          this.projectSyncService.change(project);
          this.selectProjectId = project.id;
        }
      }

      if(afterAdd) {
        this.projectSyncService.change(this.projects[this.projects.length - 1]);
        this.selectProjectId = this.projects[this.projects.length - 1].id;
      }

      if(subjectId != null) {
        const projectsub = this.projects.find(p => p.id == subjectId);
        this.projectSyncService.change(projectsub);
        this.selectProjectId = projectsub.id;
      }
    },
    (error) => {
      console.error(error);
    });
  }

  public selectProject(): void {
    if(this.selectProjectId != null) {
      const project = this.projects.find(p => p.id == this.selectProjectId);
      this.projectSyncService.change(project);
    }
  }

  public addNew(): void {
    const ref = this.modalService.open(AddModalComponent);

    ref.result.then((result) => {
      const project: Project = result;
      this.projectService.postOneProject(project, 1).subscribe((res) => {
        this.initializeProjects(true);
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public addNewTrello(): void {
    const ref = this.modalService.open(AddTrelloModalComponent);

    ref.result.then((result) => {
      const file: File = result;
      this.trelloService.migrate(file, 1).subscribe((res) => {
        this.initializeProjects(true);
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public reload($event): void {
    const project: Project = $event;
    this.initializeProjects(false, project.id);
  }

  public entryCompactViewChange(): void {
    const settings = this.localStorage.get(this.localStorageSettingsKey) as ViewSettings;
    if(settings != null) {
      this.localStorage.unset(this.localStorageSettingsKey);
      settings.entryCompactView = this.entryCompactView;
      settings.tableTypeView = settings.tableTypeView;
      this.localStorage.set({
        key: this.localStorageSettingsKey,
        value: settings,
        expirationMinutes: 0
      }); 
    } else {
      const newSettings: ViewSettings = {
        entryCompactView: this.entryCompactView,
        tableTypeView: false
      };
      this.localStorage.set({
        key: this.localStorageSettingsKey,
        value: newSettings,
        expirationMinutes: 0
      });
    }
    this.loadViewSettings();
  }

  public tableTypeViewChange(): void {
    const settings = this.localStorage.get(this.localStorageSettingsKey) as ViewSettings;
    if(settings != null) {
      this.localStorage.unset(this.localStorageSettingsKey);
      settings.entryCompactView = settings.entryCompactView;
      settings.tableTypeView = this.tableTypeView;
      this.localStorage.set({
        key: this.localStorageSettingsKey,
        value: settings,
        expirationMinutes: 0
      }); 
    } else {
      const newSettings: ViewSettings = {
        entryCompactView: false,
        tableTypeView: this.tableTypeView
      };
      this.localStorage.set({
        key: this.localStorageSettingsKey,
        value: newSettings,
        expirationMinutes: 0
      });
    }
  }
}
