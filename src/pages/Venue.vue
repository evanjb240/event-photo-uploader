<template>
    <div>
      <HeroImage text="Dreams Royal Beach" :image="`url('/images/dreams_royal_beach_overhead_view.webp')`" />
      <div>
        <div>
          We have selected Dreams Royal Beach by Hyatt in Punta Cana! Dreams Royal Beach is an all-inclusive resort located on the palm-studded white-sands of Bavaro Beach. It is an intimate, family-friendly resort that includes tons of amenities.
        </div>
        <div>
          We would recommend staying from January 5th to January 8th, but feel free to stay longer!
        </div>
        <div>
          For those traveling without kids, there is also an option to stay at Secrets Royal Beach. We ____ discount rates. Please use the booking code ___.
        </div>
        <h3>Booking</h3>
        <ul>
          <li>Resort: <a href="https://www.amrcollection.com/en/resorts-hotels/dreams/dominican-republic/royal-beach-punta-cana/" target="_blank">Dreams Royal Beach, Punta Cana</a></li>
          <li><b>DISCOUNT CODE:</b> <span class="warning"><b>ABC123</b></span></li>
        </ul>
      </div>
    </div>
</template>
<script setup>
import HeroImage from '@/components/HeroImage.vue';
</script>
<style scoped>
.warning{
  color:red
}
</style>