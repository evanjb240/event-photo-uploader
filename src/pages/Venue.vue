<template>
    <div>
      <HeroImage text="Dreams Royal Beach" :image="`url('@/public/dreams_royal_beach_overhead_view.webp')`" />
      <div>
        <h3>Booking</h3>
        <ul>
          <li>Resort: <a href="https://www.amrcollection.com/en/resorts-hotels/dreams/dominican-republic/royal-beach-punta-cana/" target="_blank">Dreams Royal Beach, Punta Cana</a></li>
          <li><b>DISCOUNT CODE:</b> <span class="warning"><b>ABC123</b></span></li>
          <li>Minimum suggested stay: <b>1/6/24</b>(afternoon arrival)-<b>1/8/24</b> (morning departure)</li>
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