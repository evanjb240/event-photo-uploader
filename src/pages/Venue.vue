<template>
    <div>
      <HeroImage text="Dreams Royal Beach" :image="`url('/images/dreams_royal_beach_overhead_view.webp')`" />
      <div>
        <p>
          We have selected Dreams Royal Beach by Hyatt in Punta Cana! Dreams Royal Beach is an all-inclusive resort located on the palm-studded white-sands of Bavaro Beach. It is an intimate, family-friendly resort that includes tons of amenities.
        </p>
        <p>
          Check out the room types and amenities here
        </p>
        <div class="primary-text centered">
            <a class="primary-button" href="https://www.amrcollection.com/en/resorts-hotels/dreams/dominican-republic/royal-beach-punta-cana/" target="_blank">Dreams Royal Beach</a>
        </div>
        <p>There is an option to stay club level or preferred. This option gives you exclusive access to certain features of the resort and a personal butler. We enjoyed having a personal butler, but it is not necessary.</p>
        <p>
          For those traveling without kids, there is also an option to stay at Secrets Royal Beach.
        </p>
        <h3>Booking</h3>
        <p>We have secured a group booking code that provides a discounted room rate. In order to secure your room, you must book using this code and pay a 1-night deposit. The remainder is due 14 days prior to your stay (charged to same card you booked with).  There are cancellation fees depending on when you cancel.</p>
        <p>
          We would recommend staying from January 5th to January 8th, but feel free to stay longer!
        </p>
        <p>Booking Code:</p>
        <p>Booking Notes</p>
        <ul>
          <li>Cancellation fees do apply. Before canceling contact us as we may have a guest who has not booked and could use your room.</li>
          <li>If you do not book through the resort website, you are not considered part of the group.</li>
          <li>Three adults maximum in a room.</li>
          <li>Children are ages 12 and under. Children over 12 are considered adults but do not count towards the three adults maximum per room.</li>
        </ul>
      </div>
      <WeddingLogo/>
    </div>
</template>
<script setup>
import HeroImage from '@/components/HeroImage.vue';
</script>
<style scoped>
.warning{
  color:red
}
/*p{
  display:flex;
  justify-content: center;
  text-align: center;
}*/
</style>