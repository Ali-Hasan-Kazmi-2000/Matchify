import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { AccountsService } from './_services/accounts.service';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavComponent, HomeComponent, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private account_services = inject(AccountsService);


  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString: any = localStorage.getItem('user');
    if (!userString) return;

    const user = JSON.parse(userString);
    this.account_services.currentUser.set(user);
  }
}
