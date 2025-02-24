import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})


export class NavComponent {
  account_services = inject(AccountsService);

  model: any = {}
  

  login() {
    this.account_services.login(this.model).subscribe({
      next: response => {
        console.log(response)  
      },
      error: error =>  console.log(error),
      complete: () => console.log("Request has been compeleted.")

    });
  }

  logout() {
    this.account_services.logout();
  }
}
