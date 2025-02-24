import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  account_services = inject(AccountsService);
  registerModel: any = {}
  cancelRegister = output<boolean>();

  register() {
    this.account_services.register(this.registerModel).subscribe({
      next: response => this.registerModel = response,
      error: error => console.log(error),
      complete: () => console.log("user has been registered.")
    });
    this.cancel();
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
