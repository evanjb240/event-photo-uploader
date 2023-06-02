<template>
    <div>
        <h1 class="centered">
            Gift Registry
        </h1>
        <div class="centered">
            <p>
              <b>Your presence and time with us in Punta Cana is truly enough.</b> However, if you still wish to send a gift, we are registered at Target, Amazon, and Crate & Barrel. Please click the store below.  
            </p>
        </div>
        <div class="card-list">
          <div class="centered">
            <a class="card" style="min-width:100px" href="https://www.target.com/gift-registry/gift/karla-and-evan" target="_blank"><img src="@/public/images/TargetLogo.jpg" width="75" height="75"></a>
          </div>
          <div class="centered">
            <a class="card" style="min-width:175px" href="https://www.amazon.com/wedding/share/karla-and-evan" target="_blank"><img src="@/public/images/AmazonLogo.webp" width="150" height="50"></a>
          </div>
          <div class="centered">
            <a class="card" style="min-width:175px" href="https://www.crateandbarrel.com/gift-registry/karla-nettleton-and-evan-bauer/r6731405" target="_blank"><img src="@/public/images/Crate-Barrel-Logo.png" width="150" height="80"></a>
          </div>
        </div>
        <p class="centered">
            For those who wish to give an electronic cash gift, we've set up a Cash App destination below!
        </p>
        <div class="centered">
          <a class="cashapp" style="min-width:175px" href="https://cash.app/$EvanKarlaWedding/25.00" target="_blank"><img src="@/public/images/CashApp.png" width="175" height="225"></a>
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