import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})


export class NavComponent {
  account_services = inject(AccountsService);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  model: any = {}
  

  login() {
    this.account_services.login(this.model).subscribe({
      next: _ => {
        this.router.navigateByUrl("/members");
      },
      error: error => this.toastr.error(error.error),
      complete: () => console.log("User has been loggedin.")

    });
  }

  logout() {
    this.account_services.logout();
    this.router.navigateByUrl("/");
  }
}
