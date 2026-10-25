#!/usr/bin/env bash
# Runs inside the emulator session started by the workflow.
set -uo pipefail

adb wait-for-device
adb install -r app-debug.apk

appium --log appium.log &
APPIUM_PID=$!

# wait until Appium answers, up to about 60 seconds
for i in $(seq 1 30); do
  curl -sf http://127.0.0.1:4723/status > /dev/null && break
  sleep 2
done

export Appium__ServerUrl=http://127.0.0.1:4723
export Appium__Udid=emulator-5554
export Appium__DeviceName=emulator-5554

dotnet test tests/MobileTests.csproj -c Release --no-build \
  --filter 'Category=Smoke' \
  --logger 'trx;LogFileName=results.trx'
TEST_EXIT=$?

kill $APPIUM_PID || true
exit $TEST_EXIT
