<template>
    <div>
        <h1 class="centered">
            Gift Registry
        </h1>
        <div class="centered">
            <p>
              We will be flying to Montana and will have very limited space. As such, we are respectfully asking for monetary gifts only, if you so choose.  
            </p>
        </div>
        <div class="centered">
          <p>
              Bring your dollars though! The reception will include ways for you to provide your monetary gifts. Get ready to do the dollar dances with the bride and groom, and "vote" on how we should spend your gift!
            </p>
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