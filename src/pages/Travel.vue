<template>
    <div class="travel-container">
        <h1 class="centered">
            Travel
        </h1>
        <WeddingLogo :width="150" :height="150"/>
        <hr/>
        <div>
            <h3>Wedding</h3>
            <p>Our wedding will be on Sunday, January 7, 2024. However, everyone is invited to the rehearsal dinner on Saturday, January 6, 2024.  We recommend flying in on January 5th, but no later than January 6th so you do not miss out on any of the festivities!</p> 
            <p>We hope you will treat this as a vacation and spend some time with us at the resort. We recommend staying January 5, 2024 to January 8, 2024.</p>
        </div>
        <div>
            <h3>RSVP Deadline</h3>
            <p>Please RSVP no later than October 1, 2023. Your RSVP is not considered complete until you have a room booked at the resort.</p>
            <p>Please click <NuxtLink to="/RSVP">HERE</NuxtLink> to RSVP.</p>
            <p>Both the rehearsal dinner and wedding dinner will be buffet style. Please note any dietary restrictions when you RSVP.</p>
        </div>
        <div>
            <h3>Venue</h3>
            <p>Our venue is Dreams Royal Beach. It is an all inclusive resort in the Hyatt family. Visit our <RouterLink to="/Venue">Venue Page</RouterLink> to find out more and book your stay!</p>
        </div>
        <div>
            <h3>Passport, Immigration & Customs</h3>
            <p>A passport is required to travel to and from Punta Cana from the U.S. Passports are taking an average of 8 weeks to be completed. We recommend applying for your passport as soon as possible.</p>
            <p>You will need to fill out immigration and custom forms to enter Punta Cana. See our <RouterLink to="/FAQ">Tips and Tricks and FAQ page</RouterLink> for more details!</p>
        </div>
        <div>
            <h3>Airport</h3>
            <p>You will need to fly into Punta Cana International Airport (PUJ).  The airport is about 20-25 minutes away from the resort.</p>
        </div>
        <div>
            <h3>Transportation</h3>
            <p>You will need to book transportation from the Airport to the resort. We recommend ____. The cost is ___. There is no need to get a rental car.</p>
        </div>  
        <WeddingLogo/>
    </div>
</template>
<style scoped>
.travel-container{
    display:flex;
    flex-direction: column;
}
</style>