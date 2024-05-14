<template>
    <div>
        <div class="name-display centered">Evan and Karla</div>
        <div>
            <div class="centered date">JANUARY 7, 2024</div>
            <div class="centered date">PUNTA CANA, DR</div>
        </div>
        <HeroImage style="margin-top:30px;" text="Montana" link="/Montana" height="600px" :image="`url('/images/Karla-Evan-Lakefront-Dip.jpg')`" />
        <WeddingLogo/>
    </div>
</template>
<style scoped>
.name-display{
    font-family: 'MoonTime';
    font-size:100px;
}
.date{
    font-family: 'Barlow C Light';
    font-size: 30px;
    margin-top:5px;
    margin-bottom:5px;
}

</style>