<template>
    <div class="travel-container">
        <HeroImage text="Montana Reception" :image="`url('/images/lonepinedusk.jpeg')`" />
        <h1 class="centered">
            Lone Pine Barn
        </h1>
        <div class="bottom-spacing">
            <div class="centered" style="gap: 50px">
                <div>
                    <div class="centered date">August 24, 2024</div>
                    <div class="centered date">1825 Wooten Rd</div>
                    <div class="centered date">Helena, MT 59602</div>
                    <div class="centered date">5:00pm - 9:00pm</div>
                </div>
                <div>
                    <div class="primary-text hero-text centered">
                        <NuxtLink class="hero-action" to="/RSVP">RSVP</NuxtLink>
                    </div>
                </div>
            </div>

        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">
                What to Expect
            </h3>
            <div class="centered">
                <div class="menu-list">
                    <div>
                        <h4>General Information</h4>
                        <ul class="unordered-list">
                            <li>Food</li>
                            <li>Drinks (Beer, wine, soda, water, BYOB for anything else)</li>
                            <li>Dancing</li>
                            <li>Yard games</li>
                            <li>Pictures</li>
                        </ul>
                    </div>
                    <div>
                        <h4>Food Menu (buffet style)</h4>
                        <ul class="unordered-list">
                            <li>TBD</li>
                        </ul>
                    </div>
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
            <p class="centered"><b>There is no shuttles available for this event.</b></p>
            <p class="centered">For those coming in from out of town, the closest hotels are a short 10 minute drive from the venue near Helena airport. (Home2 Suites, Comfort Suites, Holiday Inn)</p>
            
        </div>  
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">Attire</h3>
            <p class="centered">Whatever you will feel comfortable wearing for pictures but also playing games and dancing!</p>
           
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">Food</h3>
            <p class="centered">We're working on caterers now. Please check back later!</p>
        </div>
        <WeddingLogo/>
    </div>
</template>
<script setup>
import HeroImage from '@/components/HeroImage.vue';
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

.menu-list{
    display:flex;
    text-align: left;
    gap: 50px;
}
.date{
    font-family: 'Barlow C Light';
    font-size: 30px;
    margin-top:5px;
    margin-bottom:5px;
}

@media screen and (max-width: 800px) {
  .menu-list {
    flex-direction:column;
  }
}

.unordered-list{
    text-align: left;
}

.hero-text .hero-action {
  border: 2px solid white;
  background: rgba(10, 10, 10, 0.5);
  outline: 1;
  display: inline-block;
  padding: 10px 25px;
  color: white;
  text-align: center;
  cursor: pointer;
}

.hero-text .hero-action:hover {
  background-color: #555;
  color: white;
}
</style>