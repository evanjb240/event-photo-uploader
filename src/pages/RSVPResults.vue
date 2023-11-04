
<template>
    <div>
        <h1 class="centered">RSVP Results - Accept</h1>
        <table>
            <tr>
                <th>{{rsvpResultsAccept.length}}</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Travel Dates</th>
                <th>RSVP</th>
            </tr>
            <tr v-for="(rsvp, index) of rsvpResultsAccept">
                <td>{{ index + 1 }}</td>
                <td>{{rsvp.firstName}}</td>
                <td>{{rsvp.lastName}}</td>
                <td>{{rsvp.arrivalDate + ' - ' + rsvp.departureDate}}</td>
                <td>{{rsvp.rsvpDecision}}</td>
            </tr>
        </table>
        <h1 class="centered">RSVP Results - Decline</h1>
        <table>
            <tr>
                <th>{{rsvpResultsDecline.length}}</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>RSVP</th>
            </tr>
            <tr v-for="(rsvp, index) of rsvpResultsDecline">
                <td>{{ index + 1 }}</td>
                <td>{{rsvp.firstName}}</td>
                <td>{{rsvp.lastName}}</td>
                <td>{{rsvp.rsvpDecision}}</td>
            </tr>
        </table>
        <h1 class="centered">RSVP Results - Montana</h1>
        <table>
            <tr>
                <th>{{rsvpResultsMontana.length}}</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>RSVP</th>
            </tr>
            <tr v-for="(rsvp, index) of rsvpResultsMontana">
                <td>{{ index + 1 }}</td>
                <td>{{rsvp.firstName}}</td>
                <td>{{rsvp.lastName}}</td>
                <td>{{rsvp.rsvpDecision}}</td>
            </tr>
        </table>
        <h1 class="centered">RSVP Results - No answer</h1>
        <table>
            <tr>
                <th>{{rsvpResultsNoAnswer.length}}</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>RSVP</th>
            </tr>
            <tr v-for="(rsvp, index) of rsvpResultsNoAnswer">
                <td>{{ index + 1 }}</td>
                <td>{{rsvp.firstName}}</td>
                <td>{{rsvp.lastName}}</td>
                <td>{{rsvp.code}}</td>
            </tr>
        </table>
    </div>
</template>
<script setup>
    const rsvpResultsAccept = ref([]);
    const rsvpResultsDecline = ref([]);
    const rsvpResultsMontana = ref([]);
    const rsvpResultsNoAnswer = ref([]);

    function getRSVPResults() {
        fetch('/api/RSVPResults', {
            method: 'POST',
            headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            },
            body: 'password'
        }).then((response) => response.json())
            .then((data) => {
                rsvpResultsAccept.value = data.foundEntities.filter(x => x.rsvpDecision == 'Accept');
                rsvpResultsDecline.value = data.foundEntities.filter(x => x.rsvpDecision == 'Decline');
                rsvpResultsMontana.value = data.foundEntities.filter(x => x.rsvpDecision == 'Montana');
                rsvpResultsNoAnswer.value = data.foundEntities.filter(x => !x.rsvpDecision);
            })
            .catch((error) => {
            });
    }
    getRSVPResults();

</script>
<style>
table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td, th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

tr:nth-child(even) {
  background-color: #dddddd;
}
</style>