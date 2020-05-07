import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-page-body',
  templateUrl: './page-body.component.html',
  styleUrls: ['./page-body.component.scss']
})
export class PageBodyComponent implements OnInit {

  @Input()
  public justify: string = 'center';

  @Input()
  public align: string = 'center';

  constructor() { }

  ngOnInit(): void {
  }

}
