# Mobile CI/CD demo: Flutter app + Appium (C#) + GitHub Actions

A tiny Flutter app (login screen, then a home screen) with ONE Appium smoke test and a pipeline that:

1. builds the app into an APK,
2. starts an Android emulator,
3. starts Appium,
4. runs the test,
5. saves results, screenshots and the Appium log.

## Repository layout

```
.github/workflows/mobile-ci.yml   the pipeline
app_src/lib/main.dart             the Flutter app source
ci/run-tests.sh                   starts Appium and runs dotnet test inside the emulator
tests/                            C# xUnit + Appium test project (page objects, one test)
```

The pipeline generates the Android project around `app_src` with `flutter create`, so you only
commit the Dart source. In a real project you commit the whole Flutter project instead.

## Try it

1. Create an empty repository on GitHub.
2. Copy every file from this folder into it (keep the folder structure, including `.github`).
3. Commit and push to the `main` branch.
4. Open the **Actions** tab and watch the `mobile-ci` workflow. The first run takes about 15 to 25 minutes.
5. When it finishes, open the run and download the `test-results` artifact.

You can also start it by hand: Actions > mobile-ci > Run workflow.

## The test

`tests/Tests/LoginTests.cs` logs in with `demo` / `Password123` and checks that the home screen
shows a greeting starting with `Hi,`. It is tagged `Smoke`, which the pipeline uses as a filter.

## Run it on your own machine

1. Install Flutter, then in an empty folder run:
   `flutter create --platforms=android --project-name demo_app --org com.example.demo app`
   and copy `app_src/lib/main.dart` over `app/lib/main.dart`.
2. `cd app && flutter build apk --debug`
3. Start an emulator or connect a phone, then `adb install -r build/app/outputs/flutter-apk/app-debug.apk`
4. Start Appium in another terminal: `appium`
5. From the repository root: `dotnet test tests/MobileTests.csproj --filter "Category=Smoke"`

If your device id is not `emulator-5554`, set `Appium__Udid` (and `Appium__DeviceName`) first.

## Next steps

- Add a failing-login test and a second page object.
- Add `schedule:` to the workflow for a nightly run.
- Replace the demo app with your own app's APK.
