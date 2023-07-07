
<template>
    <div>
        <h1 class="centered">RSVP Results</h1>
        <table>
            <tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>RSVP</th>
            </tr>
            <tr v-for="rsvp of rsvpResults">
                <td>{{rsvp.firstName}}</td>
                <td>{{rsvp.lastName}}</td>
                <td>{{rsvp.rsvpDecision}}</td>
            </tr>
        </table>
        <WeddingLogo/>
    </div>
</template>
<script setup>
    const rsvpResults = ref([]);

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
                rsvpResults.value = data.foundEntities;
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