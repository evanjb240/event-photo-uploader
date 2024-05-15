<template>
    <div>
        <div class="form-container">
            <div class="wrapper">
                <ul class="steps">
                    <li :class="{isActive: activeStep==1}">Step 1</li>
                    <li :class="{isActive: activeStep==2}">Step 2</li>
                    <li :class="{isActive: activeStep==3}">Step 3</li>
                    <li :class="{isActive: activeStep==4}">Done!</li>
                </ul>
                <form class="form-wrapper">
                    <div class="section" v-if="activeStep == 1" :class="{isActive: activeStep == 1}">
                        <div class="row">
                            <div>
                                <input type="text" placeholder="Last Name" v-model="formData.LastName" />
                            </div>
                            <div>
                                <input type="text" placeholder="Enter your code" v-model="formData.Code" />
                            </div>
                        </div>
                        <div v-if="inviteValidation">An error has occurred. Please make sure all fields are filled in.</div>
                        <div>
                            <div class="button" @click="inviteLookup">Lookup Invitation</div>
                        </div>
                    </div>
                    <div class="section" v-if="activeStep == 2" :class="{isActive: activeStep == 2 }">
                        <div v-for="invite of foundInvites">
                            <h3>Will {{invite.firstName}} {{invite.lastName}} attend?</h3>
                            <div class="row cf">
                                <div class="six col">
                                    <input type="radio" :name="`rsvp_montana${invite.rowKey}`" :id="`rsvp_montana${invite.rowKey}`" value="Montana" v-model="invite.rsvpDecision">
                                    <label :for="`rsvp_montana${invite.rowKey}`">
                                        <h4 class="centered">Montana Reception</h4>
                                    </label>
                                </div>
                                <div class="six col">
                                    <input type="radio" :name="`rsvp_decline${invite.rowKey}`" :id="`rsvp_decline${invite.rowKey}`" value="Decline" v-model="invite.rsvpDecision">
                                    <label :for="`rsvp_decline${invite.rowKey}`">
                                        <h4 class="centered">Regretfully Decline</h4>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="row button-secondary" @click="back">Back</div>
                            <div class="row button" @click="checkAttendance">Next</div>
                        </div>
                    
                    </div>
                    <div class="section" v-if="activeStep == 3" :class="{isActive: activeStep == 3 }">
                        <div v-for="invite of foundInvites" >
                            <div v-if="invite.rsvpDecision == 'Accept'">
                                <div>
                                    <h3>Please add an arrival/departure date for: <b>{{`${invite.firstName} ${invite.lastName}`}}</b>:</h3>
                                </div>
                                <div class="centered">
                                    <div class="col">
                                        <DatePicker v-model:date="invite.arrivalDate"></DatePicker>
                                        <label><h4>Arrival Date</h4></label>
                                    </div>
                                    <div class="col">
                                        <DatePicker v-model:date="invite.departureDate"></DatePicker>
                                        <label><h4>Departure Date</h4></label>
                                    </div>
                                </div>
                                <div>
                                    <input type="text" placeholder="Dietary Restrictions" v-model="invite.dietaryRestrictions" />
                                </div>
                            </div>
                            <div v-if="invite.rsvpDecision == 'Decline'">
                                <label><b>{{`${invite.firstName} ${invite.lastName}`}}</b>, we're sorry to hear that you will not be coming!</label>
                            </div>
                            <div v-if="invite.rsvpDecision == 'Montana'">
                                <label><b>{{`${invite.firstName} ${invite.lastName}`}}</b>, we're still working on details for a Montana reception and will send out a separate invitation later!</label>
                            </div>
                        </div>
                        <form @submit.prevent="submit">
                            <div class="button-secondary" @click="back">Back</div>
                            <input class="button" type="submit" value="Submit" @submit.prevent="submit" />
                        </form>
                    </div>
                    <div class="section" v-if="activeStep == 4" :class="{isActive: activeStep == 4 }">
                        <h3>Thank you for your RSVP!</h3>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
<script setup>
import { reactive } from 'vue';

let formData = reactive({
    FirstName: '',
    LastName: '',
    Code: ''
});
let inviteValidation = ref(false);
let activeStep = ref(1);
let foundInvites = ref();


function inviteLookup() {
    fetch('/api/RSVPQuery', {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    }).then((response) => response.json())
        .then((data) => {
            foundInvites.value = data.foundEntities;
            if(data.status200OK){
                advanceStep();
                inviteValidation.value = false;
            }else{
                inviteValidation.value = true;
            }
        })
        .catch((error) => {
            inviteValidation.value = true;
        });
}
function checkAttendance(){
    advanceStep();
}

function submit(e) {
    fetch('/api/RSVPSubmit', {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
        body: JSON.stringify(foundInvites.value)
    }).then((response) => response.json())
        .then((data) => {
            advanceStep();
        })
        .catch((error) => {
            console.log(error);
        });
}

function advanceStep(e){
    activeStep.value = activeStep.value + 1;
}
function back(e){
    activeStep.value = activeStep.value - 1;
}

</script>
<style scoped>

h1, h2, h3, h4, h5 ,h6{
  font-weight: 200;
}

a{
  text-decoration: none;
}

p, li, a{
  font-size: 14px;
}

fieldset{
  margin: 0;
  padding: 0;
  border: none;
}

/* GRID */

.twelve { width: 100%; }
.eleven { width: 91.53%; }
.ten { width: 83.06%; }
.nine { width: 74.6%; }
.eight { width: 66.13%; }
.seven { width: 57.66%; }
.six { width: 49.2%; }
.five { width: 40.73%; }
.four { width: 32.26%; }
.three { width: 23.8%; }
.two { width: 15.33%; }
.one { width: 6.866%; }

/* COLUMNS */

.col {
	display: block;
	float:left;
	margin: 0 0 0 1.6%;
}

.col:first-of-type {
  margin-left: 0;
}

.form-container{
  width: 100%;
}

.row{
  padding: 20px 0;
}

/* CLEARFIX */

.cf:before,
.cf:after {
    content: " ";
    display: table;
}

.cf:after {
    clear: both;
}

.cf {
    zoom: 1;
}

.wrapper{
  width: 100%;
  margin: 30px 0;
}

/* STEPS */

.steps{
  list-style-type: none;
  margin: 0;
  padding: 0;
  background-color: #fff;
  text-align: center;
}


.steps li{
  display: inline-block;
  margin: 20px;
  color: #ccc;
  padding-bottom: 5px;
}

.steps li.isActive{
  border-bottom: 1px solid #3498db;
  color: #3498db;
}

/* FORM */

.form-wrapper .section{
  padding: 30px 20px 30px 20px;
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
  background-color: #fff;
  opacity: 0;
  -webkit-transform: scale(1, 0);
  -ms-transform: scale(1, 0);
  -o-transform: scale(1, 0);
  transform: scale(1, 0);
  -webkit-transform-origin: top center;
  -moz-transform-origin: top center;
  -ms-transform-origin: top center;
  -o-transform-origin: top center;
  transform-origin: top center;
  -webkit-transition: all 0.5s ease-in-out;
  -o-transition: all 0.5s ease-in-out;
  transition: all 0.5s ease-in-out;
  text-align: center;
  width: 100%;
  min-height: 300px;
}
.form-wrapper .section.isActive{
  opacity: 1;
  -webkit-transform: scale(1, 1);
  -ms-transform: scale(1, 1);
  -o-transform: scale(1, 1);
  transform: scale(1, 1);
}

.form-wrapper .button, .form-wrapper .submit{
  background-color: #3498db;
  display: inline-block;
  padding: 8px 30px;
  color: #fff;
  cursor: pointer;
  font-size: 14px !important;
  font-family: 'Open Sans', sans-serif !important;
  bottom: 20px;
  margin-top:10px;
  border-style: none;
}

.form-wrapper .button-secondary, .form-wrapper .submit{
  color: #3498db;
  display: inline-block;
  padding: 8px 30px;
  background-color: #fff;
  cursor: pointer;
  font-size: 14px !important;
  font-family: 'Open Sans', sans-serif !important;
  bottom: 20px;
  margin-top:10px;
  border-style: none;
}

.form-wrapper .submit{
  border: none;
  outline: none;
  -webkit-box-sizing: content-box;
  -moz-box-sizing: content-box;
  box-sizing: content-box;
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
}

.form-wrapper input[type="text"],
.form-wrapper input[type="password"]{
  display: block;
  padding: 10px;
  margin: 10px auto;
  background-color: #f1f1f1;
  border: none;
  width: 50%;
  outline: none;
  font-size: 14px !important;
  font-family: 'Open Sans', sans-serif !important;
}

.form-wrapper input[type="radio"]{
  display: none;
}

.form-wrapper input[type="radio"] + label{
  display: block;
  border: 1px solid #ccc;
  width: 100%;
  max-width: 100%;
  padding: 10px;
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
  cursor: pointer;
  position: relative;
}

.form-wrapper input[type="radio"] + label:before{
  content: "✔";
  position: absolute;
  right: -10px;
  top: -10px;
  width: 30px;
  height: 30px;
  line-height: 30px;
  border-radius: 100%;
  background-color: #3498db;
  color: #fff;
  display: none;
}

.form-wrapper input[type="radio"]:checked + label:before{
  display: block;
}

.form-wrapper input[type="radio"] + label h4{
  margin: 15px;
  color: #ccc;
}

.form-wrapper input[type="radio"]:checked + label{
  border: 1px solid #3498db;
}

.form-wrapper input[type="radio"]:checked + label h4{
  color: #3498db;
}
</style>