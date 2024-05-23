<template>
    <div>
        <h1 class="centered">
            Gift Registry
        </h1>
        <div class="centered">
            <p>
              Thank you so much for always being there for us. Your presence is the greatest gift. However, if you choose to honor us with a gift, we respectfully ask for a monetary contribution to our future together. We will be flying to Montana and will have very limited space.  
            </p>
        </div>
        <div class="centered">
          <p>We highly encourage you to bring your dollars! A wishing well will be available at the reception for your contributions and best wishes. The wishing well will include collection boxes for a few of our life goals. Our guests will be able to contribute to the life goal they most want to see us accomplish next. We will tally the boxes up and announce a winner. We also plan on doing dollar dances with the Bride and Groom.</p>
        </div>
        <div class="centered">
          <p>Thank you for your continued support and love. We cannot wait to celebrate with you all!</p>
        </div>
        <WeddingLogo/>
    </div>
</template>
<script setup>
</script>
<style scoped>

.card-list {
  margin: 20px;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  column-gap: 20px;
  justify-content: center;
}
.card {
  margin:20px;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
  width: 200px;
  height: 100px;
  background-color: white;
  display: flex;
  align-items: center;
  justify-content: center;
}
.cashapp {
  margin:20px;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
  width: 200px;
  height: 300px;
  background-color: white;
  display: flex;
  align-items: center;
  justify-content: center;
}

.card:hover {
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
}
p{
  font-size: 18px;
}
</style>