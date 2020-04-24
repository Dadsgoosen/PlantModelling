import { Component } from '@angular/core';
import {MenuItem, menuItems} from '../menu-items';
import {Router} from '@angular/router';

@Component({
  selector: 'app-side-nav-menu',
  templateUrl: './side-nav-menu.component.html',
  styleUrls: ['./side-nav-menu.component.scss']
})
export class SideNavMenuComponent {
  public menuItems: MenuItem[] = menuItems;

  constructor(private _router: Router) {
  }

  public navigate(link: string): void {
    this._router.navigateByUrl(link);
  }

}
