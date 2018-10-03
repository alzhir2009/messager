let recipientList = [];

var vm = new Vue({
    el: '#main',
    data: {
        apiUrl: 'http://localhost:58302/api/message',
        bodyText: '',
        subjectText: '',
        recipients: recipientList,
        errorMessage: '',
        messageResult: ''
    },
    computed: {
        canSendMessage: function () {            
            return this.apiUrl.length > 0 && this.bodyText.length > 0 && this.recipients.length > 0;
        }        
    },
    methods: {
        sendMessage: function () {
            axios.post(this.apiUrl,
                {
                    body: this.bodyText,
                    subject: this.subjectText,
                    recipients: this.recipients.map(x => x.recipientId)
                })
                .then(function (response) {
                    vm.errorMessage = '';
                    vm.messageResult = response.data;
                })
                .catch(function (error) {
                    vm.errorMessage = 'Something went wrong';
                    vm.messageResult = '';
                });
        }
    }
});


Vue.component('recipient-item', {
    props: ['recipient'],
    template: '\
    <li>\
      {{ recipient }}\
      <button v-on:click="$emit(\'remove\')" class="btn btn-xs btn-danger">Удалить</button>\
    </li>\
  '   
});

new Vue({
    el: '#recipient-list',
    data: {
        newRecipientId: '',
        recipients: recipientList,
        nextRecipientId: 1
    },
    methods: {
        addNewRecipient: function () {

            if (!this.recipients.some(_ => _.recipientId === this.newRecipientId)) {
                this.recipients.push({
                    id: this.nextRecipientId++,
                    recipientId: this.newRecipientId
                });
                this.newRecipientId = '';
            }            
        }
    },
    computed: {
        canAddRecipient: function () {
            return this.newRecipientId.length > 0;
        }
    }
});
