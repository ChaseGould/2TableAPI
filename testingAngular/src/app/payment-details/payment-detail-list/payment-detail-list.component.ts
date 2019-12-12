import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from 'src/app/shared/payment-detail.service';
import { PaymentDetail } from 'src/app/shared/payment-detail.model';

@Component({
  selector: 'app-payment-detail-list',
  templateUrl: './payment-detail-list.component.html',
  styles: []
})
export class PaymentDetailListComponent implements OnInit {

  constructor(private service: PaymentDetailService) { }

  ngOnInit() {
    this.service.refreshList();
  }

  populateForm(pd:PaymentDetail){
    this.service.formData = Object.assign({},pd);
  }

  onDelete(PMId){
    if(confirm('Are you sure you want to delete this record?')){
      this.service.deletePaymentDetail(PMId)
      .subscribe(res =>{
        this.service.refreshList();
      },
        err=>{
          console.log(err);
        })
      }
  }

}
