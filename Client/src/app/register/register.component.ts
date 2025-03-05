import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  account_services = inject(AccountsService);
  private toastr = inject(ToastrService);
  registerModel: any = {}
  cancelRegister = output<boolean>();

  register() {
    this.account_services.register(this.registerModel).subscribe({
      next: response => this.registerModel = response,
      error: error => this.toastr.error(error.error),
      complete: () => console.log("user has been registered.")
    });
    this.cancel();
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
