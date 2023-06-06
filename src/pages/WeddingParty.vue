<template>
    <div>
        <h1 class="centered">
            Wedding Party
        </h1>
        <div class="party-member-list">
            <div class="party">Bridesmaids</div>
            <PartyMember class="image-item" name="Emily Bukowski" position="Maid of Honor" imageSrc="/images/WeddingParty/Emily_MaidOfHonor.png"/>
            <PartyMember class="image-item" name="Christina Hoover" position="Bridesmaid" imageSrc="/images/WeddingParty/Christina_Bridesmaid.png"/>
            <PartyMember class="image-item" name="Holly O'Shaughnessy" position="Bridesmaid" imageSrc="/images/WeddingParty/Holly_Bridesmaid.png"/>
            <PartyMember class="image-item" name="Delaney Wickum" position="Honorary Bridesmaid" imageSrc="/images/WeddingParty/Delaney_2.png"/>
            <PartyMember class="image-item" name="Maura Rich" position="Honorary Bridesmaid" imageSrc="/images/WeddingParty/Maura.png"/>
        </div>
        <div class="party-member-list">
            <div class="party">Groomsmen</div>
            <PartyMember class="image-item" name="Eric Bauer" position="Best Man"/>
            <PartyMember class="image-item" name="Cade Fontana" position="Groomsman"/>
            <PartyMember class="image-item" name="Andrew Budzizek" position="Groomsman"/>
        </div>
        <WeddingLogo/>
    </div>
</template>
<script setup>
    import PartyMember from '@/components/PartyMember.vue';
</script>
<style>

.party-member-list {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.image-item {
  width:50%;
  margin-top: 15px;
  overflow: hidden;
  background-size: cover;
  padding: 3px;
  border-radius: 4px;
  border: 2px solid #e7e7e7;
  box-shadow: 0px 3px 6px #000000a8;
}
@media screen and (max-width: 480px) {
  .image-item {
    width: 90%;
  }
}

.image-item img {
  width: 100%;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
}

.centered{
  display:flex;
  justify-content: center;
  text-align: center;
  align-items: center;
}
.party{
    font-family: 'Barlow CR';
    font-size:40px;
    padding-top:20px;
    padding-bottom:15px;
}
</style>