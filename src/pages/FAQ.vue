<template>
    <div>
        <h1 class="centered">
            FAQ's
        </h1>
        <h2 class="centered">We will continue to update this page as more questions come in!</h2>
        <div class="primary-text centered bottom-spacing">
            <a class="primary-button" href="mailto:ebauer1993@gmail.com,k.m.nettle@gmail.com?subject=I have a question!">Send us a question!</a>
        </div>
        <PalmSpacer :width="100" :height="60"/>
        <div class="question">When should I book my hotel/resort stay?</div>
        <div class="answer">We recommend booking your room as soon as possible. The resort will fill up quickly and there is nothing we can do if the resort sells out.</div>
        <div class="question">How long can I stay at the resort?</div>
        <div class="answer">You can stay at the resort as long as you want! We recommend staying from at least January 5th to January 8th.</div>
        <div class="question">Is the resort all-inclusive?</div>
        <div class="answer">The resort is all-inclusive. Once at the resort you only need money for tips and shopping.</div>
        <div class="question">Do I need a Passport?</div>
        <div class="answer">Yes. Passports are taking an average of 8 weeks. If you need to renew or apply for a passport, we recommend doing it as soon as possible. Find more information here: https://travel.state.gov/content/travel/en/passports/how-apply.html</div>
        <div class="question">Do I need immigration forms to enter Punta Cana?</div>
        <div class="answer">Yes. Punta Cana has a digital process for filling out its immigration forms. Fill out your entry forms here: https://eticket.migracion.gob.do Once you complete the forms, you will receive a QR code. Keep these QR codes (also called e-tickets) to show at immigration after your deplane.</div>
        <div class="question">Does the resort have a shuttle from the airport?</div>
        <div class="answer">No. You will need to book your transportation from the airport to the resort. We recommend using AmstarDMC.  You can book here: www.AmstarDMC.com.</div>
        <div class="answer">It will take about an hour to go through immigration and customs, so you should book your shuttle for about an hour and a half after your flight arrival time.</div>
        <div class="answer">Book your return shuttle about 3 hours prior to your departure flight time.</div>
        <div class="question">Do I have to go through customs?</div>
        <div class="answer">Yes. You will go through immigration and customs when you enter Punta Cana and when you re-enter the U.S.</div>
        <div class="question">What are “must have" items that I should pack?</div>
        <div class="answer">
            <ul>
                <li>Your party vibes!</li>
                <li>Passport</li>
                <li>Cash for tips and shopping (USD is fine)</li>
                <li>Sunscreen! (and aloe if you burn)</li>
                <li>Sunglasses</li>
                <li>Swimsuits (we recommend multiple)</li>
                <li>Light jacket (weather will be highs of 75-80 degrees, but lows of around 65 degrees)</li>
                <li>Wedding attire</li>
                <li>Dress clothes for the seafood restaurant (men need long pants)</li>
            </ul>
            <div>Optional:</div>
            <ul>
                <li>Floaties for the pool</li>
                <li>Towel (we had no issue getting these at the resort)</li>
                <li>Tennis shoes if you are going to exercise or plan to walk a lot</li>
            </ul>
        </div>
        <div class="question">What is the primary language in Punta Cana?</div>
        <div class="answer">The primary language is Spanish. However, almost everyone at the resort speaks English. </div>
        <WeddingLogo/>
    </div>
</template>
<style scoped>
.column-container{
    display:flex;
    justify-content: space-between;
}
.column{
    width:100%;
    display:flex;
    flex-direction: column;
}
.column-section{
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}

.question{
    padding: 5px;
    font-family: 'Barlow B';
    font-size: 26px;
}
.answer{
    font-family: 'Barlow CR';
    padding: 5px;
}

.bottom-spacing{
    padding-bottom:50px;
}
</style>