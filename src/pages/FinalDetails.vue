<template>
    <div class="travel-container">
        <h1 class="centered">
            Final Details
        </h1>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">
                <alert-circle-icon size="24" class="red"></alert-circle-icon>
                Immigration and Travel Authorization
                <alert-circle-icon size="24" class="red"></alert-circle-icon>
            </h3>
            <p class="centered">You will need to fill out immigration and custom forms to enter Punta Cana. Open the section below for high-level steps.</p>
            <button class="accordion" @click="toggleTravelAuthAccordion()">View Travel Authorization Instructions
                <plus-icon size="20" v-show="!showTravelAuthInstructions"></plus-icon>
                <minus-icon size="20" v-show="showTravelAuthInstructions"></minus-icon>
            </button>
            <div class="current-relevance" v-show="showTravelAuthInstructions">
                <div>Dominican Travel Authorization Form: They used to give out paper copies to fill out on the plane. They do not do that anymore. Link below to fill out the form. Print, email, and/or screenshot the QR code(s) to yourself for you (and family).
                    <div class="primary-text centered">
                        <a class="primary-button" href="https://eticket.migracion.gob.do/" target="_blank">Dominican Travel Authorization Form</a>
                    </div>
                    <ul>
                        <b><u>Things needed prior to filling out:</u></b>
                        <li>Passport</li>
                        <li>Flight number <b>if you have 1+ stops, make sure to use the <u>last flight number</u> into PUJ</b></li>
                        <li>Flight confirmation number/code</li>
                    </ul>
                    <ol>
                        <b><u>How to fill out:</u></b>
                        <li>Click the Dominican Travel Authorization Form link below</li>
                        <li>Change the language to English</li>
                        <li>Click E-ticket Application</li>
                        <li>Click Nuevo eTicket</li>
                        <li>Change the language to English again</li>
                        <li>Pause after switching language! IF you are filling as a couple/family, you can do so, but make sure you click the toggle on the very first screen after clicking "Nuevo eTicket"</li>
                        <li>Each person you fill out for, choose "Arrival to the Dominican Republic". Fill out the rest of the various forms</li>
                        <li>When completed, download the pdf of your eTickets first, then send to email too.</li>
                    </ol>
                </div>
            </div>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">
                <alert-circle-icon size="24" class="red"></alert-circle-icon>
                Transportation
                <alert-circle-icon size="24" class="red"></alert-circle-icon>
            </h3>
            <p class="centered">You will need to book transportation to and from the resort.</p>
            <button class="accordion" @click="toggleTransportationAccordion()">View Transportation Guide
                <plus-icon size="20" v-show="!showTransportation"></plus-icon>
                <minus-icon size="20" v-show="showTransportation"></minus-icon>
            </button>
            <div class="current-relevance" v-show="showTransportation">
                <p class="centered">It will take about an hour to go through immigration and customs, so you should book your shuttle for an hour after your flight arrival time. See our Tips and Tricks page under the Entry and Exit section, to view the transportation vendors we used.</p>
                <p class="centered">If you use AmstarDMC, you must book your shuttle at least 2 weeks in advance of your arrival. Have your flight numbers and times handy.</p>
                <p class="centered">There is no need to get a rental car.</p>
                <div class="primary-text centered">
                    <a class="primary-button" href="https://www.AmstarDMC.com" target="_blank">AmstarDMC.com</a>
                </div>
            </div>
        </div>  
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">Wedding Attire</h3>
            <p class="centered">We have had a few questions about attire. Open the section below for more answers around attire.</p>
            <button class="accordion" @click="toggleAttireAccordion()">View wedding attire suggestions
                <plus-icon size="20" v-show="!showWeddingAttire"></plus-icon>
                <minus-icon size="20" v-show="showWeddingAttire"></minus-icon>
            </button>
            <div class="current-relevance" v-show="showWeddingAttire">
                <p>Our dress code is dressy casual. We want everyone to feel comfortable and express themselves, but still look nice in photos. Think of dressy casual as allowing you to express yourself while looking a little fancy.</p>
                <p>As our wedding favor to you, we will have some time set aside during the cocktail hour for you to have a professional photo taken with our greenery wall. So some guests may choose to dress more fancy for these photos - it is up to you!</p>
                <p>Examples of dressy casual for women include midi, maxi or sun dresses, skirts, or even dress pants with a statement blouse. For men, nice shorts or pants paired with a button down top (short or long sleeve), or polo.  All guests should feel free to experiment a bit more with patterns, colors, and materials. We recommend linen, but anything works! Remember the temperature will be in the 70-80s and we will be on the beach for a portion of the events.</p>
            </div>
        </div>
        <PalmSpacer v-if="false" :width="100" :height="60"/>
        <div v-if="false" class="bottom-spacing">
            <h3 class="centered">Timeline</h3>
            <p class="centered">Rehearsal dinner and wedding day timeline in section below!</p>
            <button class="accordion" @click="toggleTimelineAccordion()">View timeline details
                <plus-icon size="20" v-show="!showTimeline"></plus-icon>
                <minus-icon size="20" v-show="showTimeline"></minus-icon>
            </button>
            <div class="current-relevance" v-show="showTimeline">
                <p>Our dress code is dressy casual. We want everyone to feel comfortable and express themselves, but still look nice in photos. Think of dressy casual as allowing you to express yourself while looking a little fancy.</p>
                <p>As our wedding favor to you, we will have some time set aside during the cocktail hour for you to have a professional photo taken with our greenery wall. So some guests may choose to dress more fancy for these photos - it is up to you!</p>
                <p>Examples of dressy casual for women include midi, maxi or sun dresses, skirts, or even dress pants with a statement blouse. For men, nice shorts or pants paired with a button down top (short or long sleeve), or polo.  All guests should feel free to experiment a bit more with patterns, colors, and materials. We recommend linen, but anything works! Remember the temperature will be in the 70-80s and we will be on the beach for a portion of the events.</p>
            </div>
        </div>
        <PalmSpacer v-if="false" :width="100" :height="60"/>
        <div v-if="false" class="bottom-spacing">
            <h3 class="centered">Menus</h3>
            <p class="centered">Rehearsal dinner and wedding day menus below!</p>
            <button class="accordion" @click="toggleMenuAccordion()">View Menus
                <plus-icon size="20" v-show="!showMenus"></plus-icon>
                <minus-icon size="20" v-show="showMenus"></minus-icon>
            </button>
            <div class="current-relevance" v-show="showMenus">
                <p>Our dress code is dressy casual. We want everyone to feel comfortable and express themselves, but still look nice in photos. Think of dressy casual as allowing you to express yourself while looking a little fancy.</p>
                <p>As our wedding favor to you, we will have some time set aside during the cocktail hour for you to have a professional photo taken with our greenery wall. So some guests may choose to dress more fancy for these photos - it is up to you!</p>
                <p>Examples of dressy casual for women include midi, maxi or sun dresses, skirts, or even dress pants with a statement blouse. For men, nice shorts or pants paired with a button down top (short or long sleeve), or polo.  All guests should feel free to experiment a bit more with patterns, colors, and materials. We recommend linen, but anything works! Remember the temperature will be in the 70-80s and we will be on the beach for a portion of the events.</p>
            </div>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">Reminders</h3>
            <p class="centered">View general venue and DR reminders below</p>
            <button class="accordion" @click="toggleReminderAccordion()">View reminders
                <plus-icon size="20" v-show="!showReminders"></plus-icon>
                <minus-icon size="20" v-show="showReminders"></minus-icon>
            </button>
            <div class="current-relevance" v-show="showReminders">
                <div>
                    <h3>Venue</h3>
                    <ul>
                        <li>Do not drink the water! Even to brush your teeth. You can get bottled water everywhere, and the restaurants provide filtered water. </li>
                        <li>The DR is a developing country. Please keep this in mind when you are off the resort.</li>
                        <li>The beach is public. This means that you will be approached by locals trying to sell services or goods. If you leave the resort, you should be prepared to be approached and be cautious.</li>
                        <li>The food:
                            <ul>
                                <li>There are many food options.</li>
                                <li>Try to eat around normal times, otherwise the food may be out or old</li>
                                <li>Be conscious of the fact that you are eating different food. It may take a day or two to adjust.</li>
                            </ul>
                        </li>
                        <li>Book your spa treatments as soon as you book your room. They fill up fast!</li>
                        <li>There are a few other activities you can book with the venue for additional cost. Check out the website for more. We did a private dinner on the beach that was phenomenal!</li>
                    </ul> 
                </div>
                <div id="excursions">
                    <h3>Excursions</h3>
                    <ul>
                        <li>There are lots of excursions!
                            <ul>
                                <li>The resort has a partnership with AmstarDMC that has a lot of excursions that can be booked. You can either book ahead of time, or they have a full service concierge in the lobby upon arrival.</li>
                                <li>You can also look at Viator, as that has been a highly recommended service as well.</li>
                            </ul>
                        </li>
                    </ul>
                    <div class="button-container">
                        <div class="primary-text centered">
                            <a class="primary-button" href="https://www.AmstarDMC.com" target="_blank">AmstarDMC.com</a>
                        </div>
                        <div class="primary-text centered">
                            <a class="primary-button" href="https://www.viator.com" target="_blank">Viator</a>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
        <WeddingLogo/>
    </div>
</template>
<script setup>
import { PlusIcon } from '@zhuowenli/vue-feather-icons'
import { MinusIcon } from '@zhuowenli/vue-feather-icons'
import { AlertCircleIcon } from '@zhuowenli/vue-feather-icons'

const showTravelAuthInstructions = ref(false);
const showWeddingAttire = ref(false);
const showTransportation = ref(false);
const showTimeline = ref(false);
const showMenus = ref(false);
const showReminders = ref(false);

function toggleTravelAuthAccordion(){
    showTravelAuthInstructions.value = !showTravelAuthInstructions.value;
}
function toggleTransportationAccordion(){
    showTransportation.value = !showTransportation.value;
}
function toggleAttireAccordion(){
    showWeddingAttire.value = !showWeddingAttire.value;
}
function toggleTimelineAccordion(){
    showTimeline.value = !showTimeline.value;
}
function toggleMenuAccordion(){
    showMenus.value = !showMenus.value;
}
function toggleReminderAccordion(){
    showReminders.value = !showReminders.value;
}

</script>
<style scoped>
.travel-container{
    display:flex;
    flex-direction: column;
}
.bottom-spacing{
    padding-bottom:50px;
}
.accordion {
  display:flex;
  align-items: center;
  justify-content: space-between;
  background-color: rgb(210, 210, 210);
  color: #444;
  cursor: pointer;
  padding: 18px;
  width: 100%;
  border: none;
  text-align: left;
  outline: none;
  font-size: 20px;
  transition: 0.4s;
  font-weight: 600;
  font-family: 'Barlow CR';
  border-radius: 5px;
}

.active, .accordion:hover {
  background-color: #ccc; 
}

.current-relevance{
    background-color: azure;
}

.red{
    color:red;
}
</style>