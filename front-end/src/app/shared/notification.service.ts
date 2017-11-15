import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class NotificationService {
    constructor(private toastr: ToastrService) { }

    public notifyError(err: any): void {
        this.toastr.error(err.message, err.name);
        console.error(err);
    }
}
