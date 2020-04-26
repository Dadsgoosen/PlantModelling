import {Component, Input, OnInit} from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
  selector: 'app-page-title',
  templateUrl: './page-title.component.html',
  styleUrls: ['./page-title.component.scss']
})
export class PageTitleComponent implements OnInit {

  @Input()
  public pageTitle: string;

  constructor(private _title: Title) { }

  private setTitle(): string {
    if (this.pageTitle) { return this.pageTitle; }
    return 'Plant Visualiser';
  }

  ngOnInit(): void {
    this._title.setTitle(this.setTitle());
  }

}
