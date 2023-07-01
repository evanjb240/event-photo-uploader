<template>
    <div>
      <HeroImage text="Dreams Royal Beach" :image="`url('/images/dreams_royal_beach_overhead_view.webp')`" />
      <div>
        <p class="centered">We have selected Dreams Royal Beach by Hyatt in Punta Cana! Dreams Royal Beach is an all-inclusive resort located on the palm-studded white-sands of Bavaro Beach. It is an intimate, family-friendly resort that includes tons of amenities.</p>
        <h3>Booking</h3>
        <p>We have secured a group booking code that provides a discounted room rate. In order to secure your room, you must book using this code and pay a 1-night deposit. The remainder is due 14 days prior to your stay (charged to same card you booked with).  Cancellation and change fees apply.</p>
        <p>We would recommend staying at least from January 5th to January 8th, but feel free to stay longer! We have a discount code available for 1/3-1/9. If you want to stay outside of those dates, you will be best off calling the resort directly to book your stay.</p>
        <div class="primary-text centered">
            <a class="primary-button" href="https://www.amrcollection.com/en/resorts-hotels/dreams/dominican-republic/royal-beach-punta-cana/" target="_blank">Dreams Royal Beach</a>
        </div>
        <p>Booking Code: NBW0124</p>
        <p>Please read through the notes below before jumping to resort website. Step by step instructions are also below.</p>
        <p><u>Booking Notes:</u></p>
        <p>We recommend at least staying Jan 5th - 8th, but feel free to stay longer! We have secured a group booking code with the resort. The code applies to Jan 3 - Jan 9 with check out on the 10th. You can book outside of these dates but the code only applies to these nights. The code provides a decent discount on select rooms, but does require a deposit and has change and cancellation fees.  As an example, using the code (subject to availability) you can book a 5-night stay for 2 adults for $1,890 total ($945 per person).</p>
        <p>The code applies to the following 5 room types:</p>
        <ul>
          <li>Deluxe Family Room</li>
          <li>Deluxe Pool View (mix of kings and doubles)</li>
          <li>Deluxe Pool Terrace (mix of kings and doubles)</li>
          <li>Preferred Deluxe Swim up</li>
          <li>Preferred Deluxe Ocean view</li>
        </ul>
        <p>We recommend booking with the code as these are the cheapest rates we have found. However, the code does not guarantee availability, so we recommend booking your room within the next 3 months because the resort will begin to fill-up quickly.  Unfortunately, there is nothing we can do if the resort sells out. </p>
        <p>There is an option to stay club level or preferred. This option gives you exclusive access to certain features of the resort and a personal butler. We enjoyed having a personal butler, but it is not necessary.</p>
        <p><u>Booking notices:</u></p>
        <ul>
          <li>Cancellation and change fees apply if you use the booking code</li>
          <li>Do not book your flight through the resort. For more information on flights go to our Tips and Tricks page.</li>
          <li>If you do not book through the resort website, the information above may not apply. You also will not be listed as part of the “group” and we will have limited ability to help answer questions or deal with reservation issues.</li>
          <li>Three adults maximum in most rooms.</li>
          <li>Children are ages 12 and under. Children 2 and under are free.</li>
          <li>Children over 12 are considered adults for booking purposes. If you have children over 12, you’ll be able to book certain rooms that do not have the 3 adult maximum.</li>
          <li>You can book at the adults-only resort (Secrets) next door. Secrets’ guests have full access to Dreams. However, we do not have a discount code for Secrets at this time.</li>
        </ul>

        <p><u>How to book</u></p>
        <ul>
          <li><u>Option 1:</u> You can use our Call Center by dialing 1-866-237-3267, make sure you inform the agent of the group code or you will be charged the regular rate, only by providing the group code will the agent be able to confirm your reservation with the discounted rates.</li>
          <li><u>Option 2:</u> You can book at the website (if you are staying within the discount date range):
            <div class="primary-text centered" style="margin-bottom:20px;margin-top:20px;">
              <a class="primary-button" href="https://www.amrcollection.com/en/resorts-hotels/dreams/dominican-republic/royal-beach-punta-cana/" target="_blank">Dreams Royal Beach</a>
            </div>
            <ol>
              <li>Select the amount of guests who will occupy the room and select your travel dates between January 3 & 9, 2024</li>
              <li>Select "Resort Only". DO NOT book your flight through the resort (it's a messy process). Click the “Check Availability” button.</li>
              <li>You will be redirected to the reservation site, click on “Enter Discount Code” and type the group code all caps-no spaces as the website is caption sensitive</li>
              <li>Click on “Save” to see the rooms with the discounted rates</li>
              <li>Here you just need to select the room type you want to reserve by clicking on it and follow the reservation steps</li>
              <li>Once your 1 night deposit is charged you will receive an email with your room confirmation as well as a link to modify your reservation if needed.</li>
            </ol>
          </li>
        </ul>
        <button class="accordion" @click="toggleBookingVideo()">View Booking Instructional Video
          <plus-icon size="20" v-show="!showBookingVideo"></plus-icon>
          <minus-icon size="20" v-show="showBookingVideo"></minus-icon>
        </button>
        <div class="centered" v-show="showBookingVideo">
          <video width="75%" controls>
            <source src="https://casualstorage.blob.core.windows.net/assets/BookingVideo.mp4" type="video/mp4">
          </video>
        </div>
      </div>
      <WeddingLogo/>
    </div>
</template>
<script setup>
import HeroImage from '@/components/HeroImage.vue';
import { PlusIcon } from '@zhuowenli/vue-feather-icons'
import { MinusIcon } from '@zhuowenli/vue-feather-icons'

const showBookingVideo = ref(false);

function toggleBookingVideo(){
  showBookingVideo.value = !showBookingVideo.value;
}
</script>
<style scoped>
.warning{
  color:red
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

.panel {
  padding: 0 18px;
  display: none;
  background-color: white;
  overflow: hidden;
}
</style>