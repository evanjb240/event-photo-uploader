<template>
    <div class="travel-container">
        <h1 class="centered">
            Travel Details
        </h1>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">Suggested Travel Dates</h3>
            <p class="centered">Our wedding will be on Sunday, January 7, 2024. However, everyone is invited to the rehearsal dinner on Saturday, January 6, 2024.  We recommend flying in on January 5th, but no later than January 6th so you do not miss out on any of the festivities!</p> 
            <p class="centered">We hope you will treat this as a vacation and spend some time with us at the resort. We recommend staying January 5, 2024 to January 8, 2024.</p>
            <p class="centered">Our venue is Dreams Royal Beach, an all inclusive resort, in the Dominican Republic. Visit our Venue page to find out more and book your stay!</p>
            <div class="primary-text centered">
                <NuxtLink class="primary-button" to="/Venue">Venue</NuxtLink>
            </div>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">RSVP Deadline</h3>
            <p class="centered">Please RSVP no later than October 1, 2023. Your RSVP is not considered complete until you have a room booked at the resort.</p>
            <p class="centered">Both the rehearsal dinner and wedding dinner will be buffet style. Please note any dietary restrictions when you RSVP.</p>
            <div class="primary-text centered">
                <NuxtLink class="primary-button" to="/RSVP">RSVP</NuxtLink>
            </div>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="bottom-spacing">
            <h3 class="centered">Passport, Immigration & Customs</h3>
            <p class="centered">A passport is required to travel to and from Punta Cana from the U.S. Passports are taking an average of 8 weeks to be completed. We recommend applying for your passport as soon as possible.</p>
            <p class="centered">You will need to fill out immigration and custom forms to enter Punta Cana. See our Tips and Tricks and/or FAQ pages for more details!</p>
            <div class="button-container">
                <div class="primary-text centered">
                    <NuxtLink class="primary-button" to="/FAQ">FAQs</NuxtLink>
                </div>
                <div class="primary-text centered">
                    <NuxtLink class="primary-button" to="/TipsAndTricks">Tips and Tricks</NuxtLink>
                </div>
            </div>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div>
            <h3 class="centered">Airport</h3>
            <p class="centered">You will need to fly into Punta Cana International Airport (PUJ).  The airport is about 20-25 minutes away from the resort.</p>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div>
            <h3 class="centered">Transportation</h3>
            <p class="centered">You will need to book transportation from the Airport to the resort. It will take about an hour to go through immigration and customs, so you should book your shuttle for an hour after your flight arrival time. See our Tips and Tricks page under the Entry and Exit section, to view the transportation vendors we used.</p>
            <p class="centered">There is no need to get a rental car.</p>
            <div class="primary-text centered">
                <NuxtLink class="primary-button" to="/TipsAndTricks">Tips and Tricks</NuxtLink>
            </div>
        </div>  
        <WeddingLogo/>
    </div>
</template>
<style scoped>
.travel-container{
    display:flex;
    flex-direction: column;
}
.button-container{
    display:flex;
    justify-content: center;
    gap: 15px;
    flex-direction: row;
}
.bottom-spacing{
    padding-bottom:50px;
}
</style>